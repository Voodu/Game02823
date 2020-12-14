# quest # bear_killing # Frog hunt
VAR continue = true
-> start

=== start ===
# objective # visit_cave # Visit the Red Cave # visit # 1
~continue = false
After talking with a terridied squirrel, I was told to kill a giant frog at the Red Cave 
# dummy
{continue: -> cave}

=== cave ===
# objective # kill_bear # Kill the giant frog # kill # 1
~continue = false
That frog was really ugly and dangerous. I had to kill it. 
# dummy
{continue: -> inform}

=== inform ===
# objective # inform_man # Inform the old man # talk # 1 # dialogue # bear_killing
~continue = false
The monster is dead. I should inform the old squirrel.
# dummy 
{continue: -> finish}

=== finish ===
The squirrels in the village finally could live safely # finish
-> END