VAR name = "flower_collecting"
VAR accepted = false
VAR prize = 0
VAR completed = false
VAR exp = 20
-> start

=== start ===
- Hey, youuu! Yes, you!
  * ME: What's the problem?
- Do you like food?
  * ME: Of course
  * ME: Let's say so
  * ME: I prefer water.[] But what's with food?
- I need ingredients to make a dinner present for my mom, but I can't find some of them!
  * (accept)ME: I can help you with that.
  * (refuse)ME: Maybe another time, I'm quite busy
- {accept: 
    EXT QUEST start flower_collecting
    Yaay! Soo... I need an apple, a wine and a steak. They should be spread around the hill on the right #terminate
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
Hey, hey! Do you have all the ingredients?
+ ME: Not yet. #terminate
-> progress
* {completed}ME: Yes, here they are.
-> finished

=== finished ===
EXT QUEST progress flower_collecting inform_girl 
EXT FINISH {exp} {prize}
Thank youuu! I'll tell my mum that you helped me! #terminate
-> end_loop

=== end_loop
That dinner was so delicious! #terminate
-> end_loop