# mouse-keyboard-bot

Bot to track mouse and keyboard events and repeat them on demand.



In progress.

Actual state:
+ skeleton and libraries prepared.
+ record and repeat



# Notice: external libraries used:

- https://github.com/gmamaladze/globalmousekeyhook
- https://github.com/smchristensen/HostSwitcher


# Must be

+ record track in runtime
+ auto-repeat remembered track with 500ms time delta
+ mouse and keyboard events exactly with the same deltas as while recording

## By the way
- hotkey to focus window under mouse pointer (do simple mouse click on actual position)


# Should be

+ serialize track
- saving multiple recordings to files
- list of user-defined recordings 
- assign hotkeys to repeat recordings
- nice interface

# Could be

- area in application to show step-by-step position of mouse
- recording with screenshots to show step-by-step position of mouse with background
- saving screenshots while auto-repeating track (to find out what really happened)
- compare recorded pattern with saved auto-repeated track (screenshots)

- run next step when part of screen looks exactly like pattern
- if/else tracking basing on actual screen/part of screen pattern match
- tool to screenshot part of screen to quickly create pattern

- repeat only if screen has the same resolution (eg. notebook with and without monitor)
- repeat taking into account screen size (while recording remember screen size; while repeating rescale it to actual size)

- server-side recordings database for probably popular recordings

# Won't be

