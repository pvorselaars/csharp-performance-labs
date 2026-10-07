# Hints

<details><summary>Hint 1</summary>

Count `requestsPerOp`. What is the minimum a batch of 1 000 writes should need?
</details>

<details><summary>Hint 2</summary>

Each `SaveChanges` is a separate transaction. What does the session give you for fewer, bigger writes, and what does it cost you?
</details>

<details><summary>Hint 3</summary>

If you are only *writing* new documents and do not need change tracking, RavenDB has an API built for exactly this.
</details>
