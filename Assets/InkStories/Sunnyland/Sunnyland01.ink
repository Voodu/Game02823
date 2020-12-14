VAR name = "eagle_killing"
VAR exp = 10
VAR prize = 0
VAR completed = false
-> start

=== start ===
Woow! You are some kind of knight, ya? Someone like you can find something interesting here. Maybe you can for example help me with killing this eagle, ya?
* ME: <Continue>
- I'm afraid of going out of my house because of it! Some help would be so nice!
* ME: Okay, will do.
- EXT QUEST start eagle_killing
- Thank you so much! Just watch out for yourself! #terminate
-> progress

=== progress ===
How is it with the eagle? Is it gone?
+ ME: Not yet. #terminate
-> progress
* {completed}ME: Yes, you can freely walk around now.
EXT QUEST progress eagle_killing inform_squirrel 
EXT FINISH {exp} {prize}
-> end_loop

=== end_loop ===

Thank you so much! Visit me sometimes! #terminate
-> end_loop