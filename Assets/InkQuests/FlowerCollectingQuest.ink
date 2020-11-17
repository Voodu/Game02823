# quest # flower_collecting # Flower collecting
VAR continue = false
-> start

=== start ===
# objective # find_roses # Find 3 roses # gather # 3
# objective # find_tulips # Find 2 tulips # gather # 2
# objective # find_dandelions # Find a dandelion # gather # 1
~continue = false
After talking with a girl she asked me to find several flowers for her mom. 
# dummy
{continue: -> inform}

=== inform ===
# objective # inform_girl # Bring the flowers to the girl # talk # 1
~continue = false
I have the flowers. I should bring them to the girl. 
# dummy
{continue: -> finish}

=== finish ===
The girl was happy to have a present for her mom # finish
-> END