using System;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sample.Dtos;

namespace Sample.Controllers
{
    [Route("api/[controller]")]
    public class BlogsController : Controller
    {
        private readonly IDynamoDBContext context;
        private readonly ILogger logger;
        public BlogsController(IAmazonDynamoDB dynamoDb, ILogger<BlogsController> logger)
        {
            var config = new DynamoDBContextConfig { Conversion = DynamoDBEntryConversion.V2 };
            this.context = new DynamoDBContext(dynamoDb, config);
            this.logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> AddBlog([FromBody] Blog blog)
        {
            if (blog == null)
                return BadRequest();

            blog.Id = Guid.NewGuid().ToString();
            blog.CreatedTimestamp = DateTime.Now;

            logger.LogInformation($"Saving blog with id {blog.Id}");

            await context.SaveAsync<Blog>(blog);
            return Created("GetBlog", blog);
        }

        [HttpGet]
        public async Task<IActionResult> GetBlogs()
        {
            logger.LogInformation("Getting blogs");
            var search =  context.ScanAsync<Blog>(null);
            var page = await search.GetNextSetAsync();
            logger.LogInformation($"Found {page.Count} blogs");
            return Ok(page);
        }

        [HttpGet("{blogId}", Name = "GetBlog")]
        public async Task<IActionResult> GetBlog(string blogId)
        {

            logger.LogInformation($"Getting blog {blogId}");
            var blog = await context.LoadAsync<Blog>(blogId);
            logger.LogInformation($"Found blog: {blog != null}");

            if (blog == null)
            {
                return NotFound();
            }

            return Ok(blog);
        }

        [HttpDelete("{blogId}")]
        public async Task<IActionResult> RemoveBlog(string blogId)
        {
            logger.LogInformation($"Deleting blog with id {blogId}");
            await this.context.DeleteAsync<Blog>(blogId);

            return NoContent();
        }
    }
}