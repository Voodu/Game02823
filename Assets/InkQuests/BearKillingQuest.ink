# quest # bear_killing # Bear killing
VAR continue = true
-> start

=== start ===
# objective # visit_cave # Visit the Red Cave # visit # 1
~continue = false
After talking I was told to kill a bear at the Red Cave 
# dummy
{continue: -> cave}

=== cave ===
# objective # kill_bear # Kill the bear # kill # 1
~continue = false
The bear was bad. I had to kill it. 
# dummy
{continue: -> inform}

=== inform ===
# objective # inform_man # Inform the old man # talk # 1 # dialogue # bear_killing
~continue = false
The bear is dead. I should inform the old man.
# dummy 
{continue: -> finish}

=== finish ===
The villagers are happy now # finish
-> END