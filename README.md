KinectAdapter
=============
Owr Site: http://vova1987.github.io/KinectAdapter.github.io/
XBMC Kinect Adapter
Overview:
This project is intended to Achieve full NUI for the XBMC Media Center Application using the Microsoft Kinect device.
Hand gsetuers and voice gestures are used to pass commands to the XBMC application.
The project is based on the Fizbin Gesture Controller open source project, extending it by using the newest additon
to the XBMC SDK - the Interaction stream.

Background:
As part of our last year of studies, we have started a project meant to controll the XBMC applciation using the MS Kinect.
After searching the web for some time - we found that non of the existing oen source gesture recognition are good enough.
We have put ourselves a goal to contribute a better gesture recognition library to the open source community.

Features:
1. Large set of hand and body gestures implemented.
2. Skeleton info is combined with the UserInfo stream to provide more intuitive gestures (Grap, release, etc.)
3. Using Voice recognition of the Kinect device and the Microsoft Speech recognition library, to add even more control options
4. XBMC Command Sender that can send all existing XBMC controll commands.
5. A fully configurable user friendly gesture-to-command mapping using a dedicated XML.
6. Reusable Gesture Detectors and Command sender classes that can be plugged in into any project requiring gesture recognition

Try it out: our platform is extensible and easy to use - feel free to to fork and add your own gestures.
Learn: Take a look on the already implemented gestures - they will give you a good idea on how to implement your own gestures

We would be glad to hear any Feedback (positive or negative...) at: dvorkin1897@gmail.com
