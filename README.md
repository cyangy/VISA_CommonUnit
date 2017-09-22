#### VISA_CommonUnit
基于 VISA(Virtual Instrument Software Architecture) 的通用GPIB RS232仪器控制软件

* 已测试仪器(RS232) `Keithley 6517B`

* 已测试仪器(GPIB) `Agilent 34401A、Keithley 6517B、Keithley 195A、NF LI5640、SR830、QuadTech 7600`

* 可执行程序在VISA_CommonUnit/bin/Release/ 目录下

*  VISA_CommonUnit/bin/Release/GNUCoreutils 中的内容是 [Gow](https://github.com/bmatzelle/gow/releases) 和  [util-linux-ng](http://gnuwin32.sourceforge.net ) 的合并 
还加上 [libiconv](https://sourceforge.net/projects/gnuwin32/files/libiconv/1.9.2-1/)

* VISA_CommonUnit/bin/Release/ControlSet 中包含各仪器控制命令和可用的格式化表达式,可以自己按照`Template.commandset.template` 或 `命令集编辑模板.xlsx` 手动添加各仪器的命令集,程序启动时会在该目录下自动搜索后缀名为commandset的文件添加到程序的选项中去

* RS232示例(Keithley 6517B)

![](https://raw.githubusercontent.com/cyangy/VISA_CommonUnit/master/Examples/Keithley%20%206517B-RS232-1.png?raw=true)
![](https://raw.githubusercontent.com/cyangy/VISA_CommonUnit/master/Examples/Keithley%20%206517B-RS232-2.png?raw=true)
![](https://raw.githubusercontent.com/cyangy/VISA_CommonUnit/master/Examples/Keithley%20%206517B-RS232-3.png?raw=true)

* GPIB示例(Keithley 6517B)

![](https://raw.githubusercontent.com/cyangy/VISA_CommonUnit/master/Examples/Keithley%20%206517B-GPIB-1.png?raw=true)
![](https://raw.githubusercontent.com/cyangy/VISA_CommonUnit/master/Examples/Keithley%20%206517B-GPIB-2.png?raw=true)
![](https://raw.githubusercontent.com/cyangy/VISA_CommonUnit/master/Examples/Keithley%20%206517B-GPIB-3.png?raw=true)
