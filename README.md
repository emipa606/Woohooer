# Woohooer

![Image](https://i.imgur.com/buuPQel.png)

Update of AliceTriess mod
https://steamcommunity.com/sharedfiles/filedetails/?id=1541795354
Based on Itachi E28s updated version 
https://steamcommunity.com/sharedfiles/filedetails/?id=2127642337

![Image](https://i.imgur.com/pufA0kM.png)

	
![Image](https://i.imgur.com/Z4GOv8H.png)

B19 - Version 1.2.3 Of WooHooer!

Adds Sims (tm) like commands for woohooing with Pawns, including try for baby.

Details:
This creates context options of "Woohoo with {Other}" and "Try for Baby {Other}" with Colonists and Prisoners

Please "don't" create a cannibal farm.

Having a Baby with a prisoner will result in a Baby joining your faction. If the colonist partner is around they may grant amnesty to the mother as well (colonist recruits prisoner).

Integrations:
Birds and Bees :
Uses the Fertility Capabilities if there

Children School and Families :
If this mod is enabled will use that child generator over this mods random babies from the base game generator.

Generic:
Will Check for Capabilities named Fertility and Reproduction, which can prevent pregancy.

Warning:
Psychology mod will mess with this in a way that spams your logs.
This is because Psychology tries to Start this job without using the work driver just seeing that this joy is a valid type and trying to do in in a gathering space. Yeah psychology is trying to get your pawns to woohoo a table or campfire in a public place.

TODO:
Add Settings, note that the base game forces a second dice role on your pawns age group's fertility. Fertility Starts at 14 in base game.

Release Notes:
1.2.2 : Mod Settings

1.2.1 BugFix : Remove Marker that this is a JoyKindDef of Social, thats what causes the social spots to be woohoo-able.

1.2: Bugs and Autonomous using shared Lovin' timeout. Ruin those Rivalries with a little together time.

1.1: Thanks for finding bugs!
Changed length of laying in bed for recipient from 4000 to 400 as it lets te recipient look like theyre stuck for forever.
Fixed Using of Prisoner Beds. Removed Usage of the base game laydown and instead just have the pawns lay down directly which is awesome as I can now add woohoo spots or something.
Fixed endless toil loop for woohoo when trying to get partner to woohoo and they have a non simply interuptable job like wander.


1: No longer call Lovin at end, this is to allow the pawns to not go to bed afterwards and to not move in.

A5:
Pregnant Prisoners should spawn baby that is also a prisoner. If the other parent is Kind that pawn will be granted amnesty. Children are always given amnesty.


A4:
Major:
Animation and Motes where appropriate.
Woohooing tables and campfires with Psychology mod is at an end, causes BaseGame log message as we have to catch in pretoils as our workdriver is skipped. This is a bug with psychology. Sorry, all i can do is make it not horrible.
Minor:
Bed Theft wont happen if bed has occupents because something alone the lines of "yeah that was awkward for everyone and we agreed not to do that anymore."
WooHoo Called on Partner as well so they get the right joy.
Moodlets!

A3:
Major:
Theft of beds (including prisoner beds) for woohooing in that bed. I should add a moodlet on the owners if in line of sight
Minor:
No Med Beds
Allow Male Pregnancy with installed womb (Cost 3 Medicine, Skill level 8)
Allow prisoners to be impregnated, current default is that they are not a prisoner on spawn but a "free" pawn of the mothers faction. Will need to create a Harmony Patch in A4


A2 :
Major:
Integrate with Children and Families, may be loaded above or below; I prefer above to ensure any nonsense in the other mod always overrules
Minor:
Check if already pregnant in this rather than in letting the game codes mate do it after we try to make a baby.
this allows ui button to not show up if not pregnant.

[hr]
## Itachi E28s added info

This is just and update to the original mod WooHooer by Alycecil, In short this adds the Sims Gameplay Mechanic WooHooing. For those of you that arnt familiar with The Sims, WooHooing is The Sims version of holding hands. You Can WooHoo a female for baby hand holding. You can also WooHoo the same gender, but no baby hand holding happens.

Children School &amp; Learning Compat Still Works

Birds and Bees is UnKnown

Load Order:
Harmony
Core
Anywhere After Those Two

Bugs
UnKnown if pregnancy works, I didn't have enough patience to wait through 45 ingame days to see if a child would be born, other core features work though. Please let me know in the comments.

This is more of a By Design type of thing but you can not woohoo with anything from HAR (Humans Alien Races) Feel free to make a patch though!

Special Thanks!
Garthor On Rimworld Discord: Helped with Updating some lines of code to new rimworld mechanics
Mitz : For Fixing The Description in the Heddiff def

![Image](https://i.imgur.com/PwoNOj4.png)



-  See if the the error persists if you just have this mod and its requirements active.
-  If not, try adding your other mods until it happens again.
-  Post your error-log using https://steamcommunity.com/workshop/filedetails/?id=818773962]HugsLib and command Ctrl+F12
-  For best support, please use the Discord-channel for error-reporting.
-  Do not report errors by making a discussion-thread, I get no notification of that.
-  If you have the solution for a problem, please post it to the GitHub repository.


