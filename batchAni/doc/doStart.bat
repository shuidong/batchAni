@echo off
set TextureMerger_PATH=F:\Egret\TextureMerger\

echo doStart.bat pngFolder jsonName [frameRate=12]

rem 设置环境变量
set PATH=%PATH%;%TextureMerger_PATH%

rem 执行合图
call TextureMerger.exe -p %1 -o %1\%2.json -e /.*\.png
if Not errorlevel 0 ( 
	echo "error when run TextureMerger.exe"
	exit 1
)

rem 默认帧率=12
set rate=12
if Not "%3"=="" (
	set rate=%3
)
call batchAni.exe %1\%2.json %rate%
if Not errorlevel 0 ( 
	echo "error when run batchAni.exe"
	exit 1
)
echo %1\%2.json
