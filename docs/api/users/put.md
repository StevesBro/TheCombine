# Update user info

**URL** : `/v1/users/{userId}`

**Method** : `PUT`

**Auth required** : Yes

**Data type**: [`User`](user.md)

## Success Response

**Code** : `200 OK`

**Data content**: `id: string`

## Error Response

**Condition** : If user ID is not found

**Code** : `404 NOT FOUND`
