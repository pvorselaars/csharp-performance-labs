# Hints

<details><summary>Hint 1</summary>

Start with `requestsPerOp`; then look at allocation. Do they have the same cause?
</details>

<details><summary>Hint 2</summary>

There are three separate problems in this one method. List them before fixing any.
</details>

<details><summary>Hint 3</summary>

Each customer causes a query that downloads whole order documents to read one number. Could the server hold that number already summed?
</details>
