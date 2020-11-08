VAR name = "flower_collecting"
VAR accepted = false
VAR prize = 0
VAR completed = false
-> start

=== start ===
- Hey, youuu! Yes, you!
  * What's the problem?
- Do you like flowers?
  * Of course
  * Let's say so
  * I prefer trees.[] But what's with flowers?
- I need some for a present for my mom, but I can't find some of them!
  * (accept) I can help you with that.
  * (refuse) Maybe another time, I'm quite busy
- {accept: 
    Yaay! Soo... I need one blue, two red and one violet flower. They should be spread around the hill #terminate
    -> progress
    }
- {refuse:
    Oh... That's a pity. I hope you'll change your mind. #terminate 
    -> undecided
    }

=== undecided ===
Sooo? Will you help me?
  * Yes[.], <>
    -> start.accept
  + No[.], -> start.refuse

=== progress ===
Hey, hey! Do you have all the flowers?
+ Not yet. #terminate
-> progress
* {completed}Yes, here they are.
-> finished

=== finished ===
Thank youuu! I'll tell my mum that those are also from you! #terminate
-> end_loop

=== end_loop
Those flowers are so beautiful! #terminate
-> end_loop