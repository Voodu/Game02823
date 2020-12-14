VAR name = "getting_eq"
VAR exp = 10
VAR prize = 0
VAR completed = false
-> start

=== start ===
Hey buddy! You want to go for an adventure? I can show you something cool, but you must bring me a beer and a shield. They are up here.
* ME: What will you show me?
- It's a surprise!
* ME: I hope it'll be worth the effort
- EXT QUEST start getting_eq
- It will be, I promise! Oh, a tip - you can move that box. But don't drop it on my head!#terminate
-> progress

=== progress ===
So? Do you have everything?
+ ME: Not yet. #terminate
-> progress
* {completed}ME: Yes, here they are.
-> finished

=== finished ===
EXT QUEST progress getting_eq inform_squirrel 
EXT FINISH {exp} {prize}
Great! Now a tip - equip them. With shield you can block with E, with beer you can roll with SHIFT #terminate
-> end_loop

=== end_loop
Remeber: beer + SHIFT = roll, shield + E = block.  #terminate
-> end_loop