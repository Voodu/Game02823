# quest # eagle_killing # Eagle killing
VAR continue = true
-> start

=== start ===
# objective # kill_eagle # Kill the eagle # kill # 1
~continue = false
A friendly squirrel told me about problems with an eagle. I should get rid of it. 
# dummy
{continue: -> inform}

=== inform ===
# objective # inform_squirrel # Inform squirrel # talk # 1 # dialogue # eagle_killing
~continue = false
The eagle is dead. I should inform the squirrel about that.
# dummy 
{continue: -> finish}

=== finish ===
The squirrel is happy now # finish
-> END