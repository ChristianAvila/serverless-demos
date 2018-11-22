# Serverless example

This example consist to create a new DynamoDB table with an string key

## Deploy command line
- Install [aws cli](https://docs.aws.amazon.com/cli/latest/userguide/installing.html)
- Install [serverless](https://serverless.com/framework/docs/getting-started/)

```sh
$ serverless deploy --verbose --env=stg
```

## Clean up
- To clean up apply the following command

```sh
$ serverless remove --verbose --env=stg
```
