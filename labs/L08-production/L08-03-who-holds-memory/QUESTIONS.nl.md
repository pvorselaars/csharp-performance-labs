# Vragen
1. Welk type is goed voor de meeste **bytes**? Welk type's **aantal** groeit tussen je twee gcdumps? Zijn dat dezelfde type?
2. Sommige grote dingen zijn *niet* de lek. Welke grote objecten zijn aanwezig maar constant tussen de twee dumps? Hoe weet je dat ze niet het probleem zijn?
3. Wat is de **GC-root** van een gelekt object (root → … → object)? Welk *static field* of welke collectie is dat?
4. Er bestaan twee andere collecties die niet groeien. Wat onderscheidt ze van de lekkende?
5. Wat zou je de ontwikkelaars in één zin vertellen om te veranderen?
6. Hoe zou je voorkomen dat dit ooit live gaat (welke metriek of test zou dit opvangen)?
