# Hints

<details><summary>Hint 1</summary>

How many round trips does the export make, and why that many?
</details>

<details><summary>Hint 2</summary>

`Skip(n)` asks the server to find and discard the first n results on every call. What does that do to the later pages?
</details>

<details><summary>Hint 3</summary>

You do not need random access to pages, only to see every document once. Is there a way to read a result set as one continuous flow?
</details>
