# WorkoutLogApp
[![icon.png](https://i.postimg.cc/rw8y2yct/icon.png)](https://postimg.cc/BXVfx0jq)

Workout Diary App, Xamarin Forms.

### Introduction
Basic workout diary application known as 'Workout Log' in the Google Play store using Xamarin Forms, JSON, C# & Xaml.

### Licensing
your free to use my code however you like according to the MIT licensing.

### Images
Some images from the application to introduce the UI and its general purpose:

[![1.png](https://i.postimg.cc/nzqNL7GV/1.png)](https://postimg.cc/bDNmCZ54)
[![2.png](https://i.postimg.cc/HLyhS5K0/2.png)](https://postimg.cc/qtpwqtpg)
[![3.png](https://i.postimg.cc/br3CGRDP/3.png)](https://postimg.cc/rRRC3xNZ)
[![4.png](https://i.postimg.cc/52H70Lxc/4.png)](https://postimg.cc/ZWSFs9Bj)

### Preview
The application can be found in the Google Play store in the given url:

https://play.google.com/store/apps/details?id=com.shaharband.workoutlogger

### Possible Improvements
The application contains the default data from built in JSON file on first launch or on failed load operation. 

Providing that, the load and save operation is utilized from Xamarin Essentials User Preferences to serialize and deserialize the user data from JSON format.

Therefore, memory limitations may occur to avoid such scenarios the app limits the log diary history and removes oldest case at limit, making more dynamic operation will lead for greater capacity but for my purposes this is a sufficient solution.

The app could be improved using sorting algorithms and defining the different categories as an array looking backwards could shorten the code and support the basic principles of S.O.L.I.D.

### To clone this repository:

Write in your Git Bash the follow:

    $ git clone https://github.com/ShaharBand/WorkoutLogApp.git
