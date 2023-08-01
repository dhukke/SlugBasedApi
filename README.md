# Slug based API

Project to showcase slug based API where and item can be fetched by id or more human friendly description.

## Example

The item:

```json
{
  "id": "5872d4e0-d3a5-4f93-8e62-84652b28d160",
  "description": "Pen",
  "year": 2023,
  "slug": "pen-2023"
}
```

Could be fetched by:

<http://localhost:5110/item/pen-2023> or
<http://localhost:5110/item/5872d4e0-d3a5-4f93-8e62-84652b28d160>

## Run local

Run the command `docker-compose up` on the root folder to spin up mongodb

Run the app and access the swagger page (example: http://localhost:5260/swagger/index.html)