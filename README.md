# API Documentation

## GET /people
Retrieves all people, their interests and the person's submitted links.

## GET /people/{id}
Retrieves basic data of a person with a specific ID.

## GET /people/{id}/interests
Retrieves the interests of a person with a specific ID.

## GET /people/{id}/interests/links
Retrieves the links submitted by a person with a specific ID.

## GET /interests
Retrieves all interests.

## GET /interests{id}
Retrieves basic data of an interest with a specific ID.

## POST /interests
Adds a new interest to database.

### Example request body
```http
{
  "name": "Movies & TV-shows",
  "description": "Watching movies and TV-shows are a great pastime."
}
```

## POST /people
Adds a new person to database.

### Example request body
```http
{
"first-name": "John",
"last-name": "Doe",
"phone-number": "555-555-555-55"
}
```

## POST /people/interests
Adds a new interest to a person.

### Example request body
```http
{
  "person-id": 1,
  "interest-id": 3
}
```

## POST /people/interests/links
Adds a new link to an interest, submitted by a person.

### Example request body
```http
{
  "link": "https://www.imdb.com/",
  "interest-id": 3,
  "person-id": 1
}
```
