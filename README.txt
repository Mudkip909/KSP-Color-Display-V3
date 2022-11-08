KSP Display Version 3 for 1.12
(KSPlights3 internally)

Now in COLOR!
Demonstration video:
https://youtu.be/4d2SS2SViEA

Requires ModuleManager

This mod gets rid of the Kal Controllers used for other display types, and substitutes it for a mod.
It reads each frame from the frames/ directory, and displays it on its respective light.

HOW TO USE:

1: Conversion

You need ffmpeg to split the video into frames. run the command listed in COMMAND.txt
ffmpeg -i input.mp4 -vf scale=20:20 -vsync vfr $file%03d.png

in the command line at the frames directory. depending on the length of the video, it should make a few hundred to a few thousand frames.
The default framerate is defined as 25, however it can be changed in the fps.txt file.
Note that you can increase or decrease the scale of your video depending on the size of screen you have. More info on the screen later.

2: Playing

Once conversion is complete, you can load the craft and watch the video play on the screen.
There is a 10x10 and 20x20 display included in crafts, the 10x10 is meant for lower end computers/testing. to use this, you should set the scale in the ffmpeg step to scale=10:10

If you'd like to change the video, you don't have to restart the game, just reload the flight.



HOW THE SCREEN WORKS:
Each light now has a module called "LightIndexer". These have X and Y sliders assignable 1-64, but theoretically have an infinite range by changing the code's kspfield range (But im not doing that, as a resolution that big would have over 4096 parts. Plus, just edit the craft file.)
There are 20 columns with lights, and their y indexes range from 1-20 all the way down. One column is made with an x value of 1. this row is copied, but the X value is changed to 2, then 3, and it continues up to 20.
Then there is an avionics hub nose code with a LightController Module. This controls each light, and sets all of their RGB values for each frame. The Controller will continue until there are no more new frames, and will restart from the beginning.
