# Hints (open er één tegelijk)

<details><summary>Hint 1: welk tool?</summary>

dotMemory-compare: de groei zit in `Document` en zijn `byte[]`, maar `Metadata` groeide ook. Open het retentiepad voor een `Document`.
</details>

<details><summary>Hint 2: waar?</summary>

De dominator is de tag-table. Wat wordt gebruikt als **key**, en wat doet een `Dictionary` met zijn keys?
</details>

<details><summary>Hint 3: waarom?</summary>

Een `Dictionary<object,…>` houdt zijn keys sterk vast, dus een document kan nooit verzameld worden zolang het een key is. Je wilt een table waarvan de entries verdwijnen zodra het *key-object* onbereikbaar wordt. Kijk naar `ConditionalWeakTable<TKey,TValue>`.
</details>
