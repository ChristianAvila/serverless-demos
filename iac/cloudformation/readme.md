# CloudFormation example

This example consist to create a new DynamoDB table with an string key

## Deploy command line
- Install [aws cli](https://docs.aws.amazon.com/cli/latest/userguide/installing.html)
- After installation and key setup runs the following command

```sh
$ aws cloudformation validate-template --template-body file://./table.yaml
$ aws cloudformation create-stack --stack-name score-demo-table  --template-body file://./table.yaml --parameters file://./parameters.json
```

- Then you can check the creation of the table in AWS.

## Clean up
- To clean up apply the following command

```sh
$ aws cloudformation delete-stack --stack-name score-demo-table
```
