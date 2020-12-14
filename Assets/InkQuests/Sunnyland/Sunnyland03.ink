# quest # getting_eq # Getting equipment
VAR continue = true
-> start

=== start ===
# objective # find_beer # Find beer # gather # 1
# objective # find_shield # Find shield # gather # 1
~continue = false
Another squirrel told me that she will teach me something cool if I bring her a beer and a shield.
# dummy
{continue: -> inform}

=== inform ===
# objective # inform_squirrel # Bring items to the squirrel # talk # 1 # dialogue # getting_eq
~continue = false
I have the items. Let's show them to the squirrel.
# dummy 
{continue: -> finish}

=== finish ===
Great! With beer equipped I can roll with SHIFT, with shield I can block with E # finish
-> END