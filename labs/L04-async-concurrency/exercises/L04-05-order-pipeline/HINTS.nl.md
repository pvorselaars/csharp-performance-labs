# Hints (open er één tegelijk)

<details><summary>Hint 1: welke tool?</summary>

De `maxQueued` van de harness laat de queue-lengte zien. In dotMemory is de groei `byte[]` bereikbaar vanaf het channel. Kijk ook naar de *tempo's*: hoe snel komen items binnen, en hoe snel gaan ze eruit?
</details>

<details><summary>Hint 2: waar?</summary>

`Channel.CreateUnbounded` oefent nooit back-pressure uit op de producer. Welke *optie* verandert dat?
</details>

<details><summary>Hint 3: waarom?</summary>

Zonder back-pressure verplaatst een producer die sneller is dan de consumer simpelweg werk naar het geheugen. Een **begrensde** queue laat de producer wachten, zodat de traagste stage het tempo bepaalt en het geheugengebruik vlak blijft. Wat moet de producer gebruiken om te *wachten* in plaats van te falen als de queue vol is?
</details>
