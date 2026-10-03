# Hints

<details><summary>Hint 1</summary>

The request count is already 1. What else does a query cost besides round trips?
</details>

<details><summary>Hint 2</summary>

How many fields of each `Invoice` does the loop actually read? Count the bytes you receive against the ones you use.
</details>

<details><summary>Hint 3</summary>

Can the server send back only the fields you ask for?
</details>
