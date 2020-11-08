VAR name = "bear_killing"
VAR accepted = false
VAR prize = 0
VAR completed = false
-> start

=== start ===
Hey, you look like a tough one!
* So what?
  Hey, hey, chill out.
* Can I help you?
  Indeed, I have a problem.
* [Raise your eyebrows]
- <> I wanted to ask for help with a pesky bear out there.
It killed three people from our village already and everone is afraid of going into the woods. Do you think you can help with that?
* Sure, no problem.
  - - (accept)
  ~ accepted = true
  Thank you! You'll find that beast somewhere around the Red Cave. Good luck! #terminate
  -> progress 
* (money)What will I have from that?
  ~ prize = 100
  Nothing just for fun, he? I guess we can pay you {prize} gold. Is it ok?
  * * That's better. 
  -> accept
  + + No[.], <> -> refuse
* (refuse)I don't have time for that. #terminate
- - -> undecided

=== undecided ===
Have you maybe changed your mind and can help with that bear?
  * Yes, I'll help you.
    -> start.accept
  + Maybe. <> -> start.money
  + No[.], -> start.refuse

=== progress ===
How is it with the bear? Have you killed it?
+ Not yet. #terminate
-> progress
* {completed}Yes, it's dead.
-> finished

=== finished ===
Thank you so much! <>
{prize > 0: Here's your {prize} gold. <> }
You are always welcome here! #terminate
-> end_loop

=== end_loop ===
I'm happy to see you again! #terminate
-> end_loop