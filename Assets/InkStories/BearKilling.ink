VAR name = "bear_killing"
VAR accepted = false
VAR prize = 0
VAR exp = 50
VAR completed = false
-> start

=== start ===
Hey, you look like a tough one!
* ME: So what?
  Hey, hey, chill out.
* ME: Can I help you?
  Indeed, I have a problem.
* ME: [\[Raise your eyebrows\]]
- <> I wanted to ask for help with a pesky bear out there.
It killed three people from our village already and everone is afraid of going into the woods. Do you think you can help with that?
* ME: Sure, no problem.
  - - (accept)
  ~ accepted = true
  Thank you! You'll find that beast somewhere around the Red Cave. Good luck! 
  EXT QUEST start bear_killing #terminate
  -> progress 
* (money)ME: What will I have from that?
  ~ prize = 100
  Nothing just for fun, he? I guess we can pay you {prize} gold. Is it ok?
  * * ME: That's better. 
  -> accept
  + + ME: No[.], <> -> refuse
* (refuse)ME: I don't have time for that. #terminate
- - -> undecided

=== undecided ===
Have you maybe changed your mind and can help with that bear?
  * ME: Yes, I'll help you.
    -> start.accept
  + ME: Maybe. <> -> start.money
  + ME: No[.], -> start.refuse

=== progress ===
How is it with the bear? Have you killed it?
+ ME: Not yet. #terminate
-> progress
* {completed}ME: Yes, it's dead.
-> finished

=== finished ===
EXT FINISH {exp} {prize}
Thank you so much! <>
{prize > 0: Here's your {prize} gold. <> }
You are always welcome here!
EXT QUEST progress bear_killing inform_man #terminate
-> end_loop

=== end_loop ===
I'm happy to see you again! #terminate
-> end_loop