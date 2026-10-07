# Hints

<details><summary>Hint 1</summary>

Count `requestsPerOp`: how many of the requests were needed for the answer the job actually reports?
</details>

<details><summary>Hint 2</summary>

Which of the 100 counts does the job use? What does each of the others buy?
</details>

<details><summary>Hint 3</summary>

Indexing is asynchronous and batched. Does it help or hurt to ask for a consistent answer after each write instead of after the last one?
</details>
