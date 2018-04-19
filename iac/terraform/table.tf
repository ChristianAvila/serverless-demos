resource "aws_dynamodb_table" "demo-scores-terraform" {
  name           = "${var.table-name}"
  read_capacity  = 2
  write_capacity = 2
  hash_key       = "UserId"
  range_key      = "Game"

  attribute {
    name = "UserId"
    type = "S"
  }

  attribute {
    name = "Game"
    type = "S"
  }

  tags {
    Name        = "demo-scores-terraform"
    Environment = "demo"
  }
}
