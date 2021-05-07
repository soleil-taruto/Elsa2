CALL qq
cx **

rem	CALL DebugRelease.bat /B
rem	CALL Release.bat /B
	CALL Release.bat /V 100

C:\Factory\Petra\UpdatingCopy.exe out C:\be\Web\DocRoot\Elsa2\*
C:\Factory\Petra\RunOnBatch.exe C:\be\Web Upload.bat
