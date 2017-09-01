JimuAR
Tai An (antai.ted@gmail.com)

Vuforia + Unity 5

Using User Defined Target (Marker, book cover, etc.) for AR World Spawn. 

Platform: PC/Mac/iOS
Requirement(s): Camera x 1

It's worth noting that the default gravity direction in Unity Physics Engine will constantly be overwritten based on Camera's orientation. Thus the default rigid body physics simulation will not work properly. A workaround is to reset the gravity direction to the up vector of the SandBox object in the update function.


9/1/2017 Updated to Unity 5.6.2f1. Apple ARKit and new iPhones are coming, time to open source this project. Thanks.

