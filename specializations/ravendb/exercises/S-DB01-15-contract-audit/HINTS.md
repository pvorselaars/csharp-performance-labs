# Hints

<details><summary>Hint 1</summary>

Count `requestsPerOp` against the number of contracts.
</details>

<details><summary>Hint 2</summary>

Every call to read a revision as of a date is its own request. Is there a way to tell the server up front which revisions you will need?
</details>

<details><summary>Hint 3</summary>

RavenDB can send related revisions along with a load, the way `Include` sends related documents.
</details>
