# quest # flower_collecting # Ingredients collecting
VAR continue = false
-> start

=== start ===
# objective # find_roses # Find a apple # gather # 1
# objective # find_tulips # Find a steak # gather # 1
# objective # find_dandelions # Find a wine # gather # 1
~continue = false
After talking with a girl she asked me to find several dinner ingredients for her mom. 
# dummy
{continue: -> inform}

=== inform ===
# objective # inform_girl # Bring the ingredients to the girl # talk # 1 # dialogue # flower_collecting
~continue = false
I have the ingredients. I should bring them to the girl. 
# dummy
{continue: -> finish}

=== finish ===
The girl was happy to have a delicious meal for her mom # finish
-> END