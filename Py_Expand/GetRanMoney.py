'''
模块作者：Toy‘s World
模块功能：随机加减金钱
'''

import random

ready = random.randint(-500,500)

f = open("D:\\CLPATH.txt","r")
path = f.read()
f.close()

fr = open(path + "\\API\\money.txt","r")
before_money = int(fr.read())
now_money = before_money + ready
fr.close()

fw = open(path + "\\API\\money.txt","w")
fw.write(str(now_money))
fw.close()

fe = open(path + "\\API\\text.txt","w")
fe.write("Module RanMoney : Add " + str(ready) + " Money.")