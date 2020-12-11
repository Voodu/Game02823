VAR name = "flower_collecting"
VAR accepted = false
VAR prize = 0
VAR completed = false
VAR exp = 20
-> start

=== start ===
- Hey, youuu! Yes, you!
  * ME: What's the problem?
- Do you like flowers?
  * ME: Of course
  * ME: Let's say so
  * ME: I prefer trees.[] But what's with flowers?
- I need some for a present for my mom, but I can't find some of them!
  * (accept)ME: I can help you with that.
  * (refuse)ME: Maybe another time, I'm quite busy
- {accept: 
    EXT QUEST start flower_collecting
    Yaay! Soo... I need one blue, two red and one violet flower. They should be spread around the hill #terminate
    -> progress
    }
- {refuse:
    Oh... That's a pity. I hope you'll change your mind. #terminate 
    -> undecided
    }

=== undecided ===
Sooo? Will you help me?
  * ME: Yes[.], <>
    -> start.accept
  + ME: No[.], -> start.refuse

=== progress ===
Hey, hey! Do you have all the flowers?
+ ME: Not yet. #terminate
-> progress
* {completed}ME: Yes, here they are.
-> finished

=== finished ===
EXT QUEST progress flower_collecting inform_girl 
EXT FINISH {exp} {prize}
Thank youuu! I'll tell my mum that those are also from you! #terminate
-> end_loop

=== end_loop
Those flowers are so beautiful! #terminate
-> end_loop