# FiniteStateMachine
Implement finite state machine


``` csharp
            int timer = 0;
            const int maxTimerCount = 10;

            int timer1 = 0;
            const int maxTimer1Count = 20;
            // add new state
            machine.NewState("空闲")
                 .Initialize(() => Console.WriteLine("初始化 空闲"))
                 .Enter(() =>
                 {
                     timer = 0;
                     Console.WriteLine("进入 空闲");
                 })
                 .Update(() => Console.WriteLine("空闲... " + timer++))
                 .Exit(() => Console.WriteLine("退出 空闲"))
                 .Translate("空闲=>巡逻").Valid(() => timer >= maxTimerCount).Transfer(() => Console.WriteLine("转化中 ...")).To("巡逻");

            machine.NewState("巡逻")
                .Initialize(() => Console.WriteLine("初始化 巡逻"))
                .Enter(() =>
                {
                    timer = 0;
                    Console.WriteLine("进入 巡逻");
                })
                .Update(() => Console.WriteLine("巡逻... " + timer++ + " 危险系数" + timer1++))
                .Exit(() => Console.WriteLine("退出 巡逻"))
                .Translate("巡逻=>逃跑").Valid(() => timer1 > maxTimer1Count).To("逃跑")
                .Translate("巡逻=>空闲").Valid(() => timer >= maxTimerCount).Transfer(() => Console.WriteLine("转化中 ...")).To("空闲");

            machine.NewState("逃跑")
               .Initialize(() => Console.WriteLine("初始化 逃跑"))
               .Enter(() =>
               {
                   timer = 0;
                   Console.WriteLine("进入 逃跑");
               })
               .Update(() => Console.WriteLine("逃跑... " + timer1--))
               .Exit(() => Console.WriteLine("退出 逃跑"))
               .Translate("逃跑=>空闲").Valid(() => timer1 == 0).Transfer(() => Console.WriteLine("转化中 ...")).To("空闲");

            

            //initialize machine
            machine.Initialize();
```


``` logs



初始化 空闲
初始化 巡逻
初始化 逃跑
进入 空闲
空闲... 0
空闲... 1
空闲... 2
空闲... 3
空闲... 4
空闲... 5
空闲... 6
空闲... 7
空闲... 8
空闲... 9
退出 空闲
转化中 ...
进入 巡逻
巡逻... 0 危险系数0
巡逻... 1 危险系数1
巡逻... 2 危险系数2
巡逻... 3 危险系数3
巡逻... 4 危险系数4
巡逻... 5 危险系数5
巡逻... 6 危险系数6
巡逻... 7 危险系数7
巡逻... 8 危险系数8
巡逻... 9 危险系数9
退出 巡逻
转化中 ...
进入 空闲
空闲... 0
空闲... 1
空闲... 2
空闲... 3
空闲... 4
空闲... 5
空闲... 6
空闲... 7
空闲... 8
空闲... 9
退出 空闲
转化中 ...
进入 巡逻
巡逻... 0 危险系数10
巡逻... 1 危险系数11
巡逻... 2 危险系数12
巡逻... 3 危险系数13
巡逻... 4 危险系数14
巡逻... 5 危险系数15
巡逻... 6 危险系数16
巡逻... 7 危险系数17
巡逻... 8 危险系数18
巡逻... 9 危险系数19
退出 巡逻
转化中 ...
进入 空闲
空闲... 0
空闲... 1
空闲... 2
空闲... 3
空闲... 4
空闲... 5
空闲... 6
空闲... 7
空闲... 8
空闲... 9
退出 空闲
转化中 ...
进入 巡逻
巡逻... 0 危险系数20
退出 巡逻
进入 逃跑
逃跑... 21
逃跑... 20
逃跑... 19
逃跑... 18
逃跑... 17
逃跑... 16
逃跑... 15
逃跑... 14
逃跑... 13
逃跑... 12
逃跑... 11
逃跑... 10
逃跑... 9
逃跑... 8
逃跑... 7
逃跑... 6
逃跑... 5
逃跑... 4
逃跑... 3
逃跑... 2
逃跑... 1
退出 逃跑
转化中 ...
进入 空闲
空闲... 0
空闲... 1
空闲... 2
空闲... 3
空闲... 4
空闲... 5
空闲... 6
空闲... 7
空闲... 8
空闲... 9
退出 空闲
转化中 ...
进入 巡逻
巡逻... 0 危险系数0
巡逻... 1 危险系数1
巡逻... 2 危险系数2
巡逻... 3 危险系数3
巡逻... 4 危险系数4
巡逻... 5 危险系数5
巡逻... 6 危险系数6
巡逻... 7 危险系数7
巡逻... 8 危险系数8
巡逻... 9 危险系数9
退出 巡逻
转化中 ...
进入 空闲
空闲... 0
空闲... 1
空闲... 2
空闲... 3
空闲... 4
空闲... 5
空闲... 6
空闲... 7
空闲... 8
空闲... 9
退出 空闲
转化中 ...
进入 巡逻
巡逻... 0 危险系数10
巡逻... 1 危险系数11
巡逻... 2 危险系数12
巡逻... 3 危险系数13
巡逻... 4 危险系数14
巡逻... 5 危险系数15
巡逻... 6 危险系数16
巡逻... 7 危险系数17
巡逻... 8 危险系数18
巡逻... 9 危险系数19
退出 巡逻
转化中 ...
进入 空闲
空闲... 0
空闲... 1
空闲... 2
空闲... 3
空闲... 4
空闲... 5
空闲... 6
空闲... 7
空闲... 8
空闲... 9
退出 空闲
转化中 ...
进入 巡逻
巡逻... 0 危险系数20
退出 巡逻
进入 逃跑
逃跑... 21
逃跑... 20
逃跑... 19
逃跑... 18
逃跑... 17
逃跑... 16
逃跑... 15
逃跑... 14
逃跑... 13
逃跑... 12
逃跑... 11
逃跑... 10
逃跑... 9
逃跑... 8
逃跑... 7
逃跑... 6
逃跑... 5
逃跑... 4
逃跑... 3
逃跑... 2
逃跑... 1
退出 逃跑
转化中 ...
进入 空闲
空闲... 0
空闲... 1
空闲... 2
空闲... 3
空闲... 4
空闲... 5
空闲... 6
空闲... 7
空闲... 8
空闲... 9
退出 空闲
转化中 ...
进入 巡逻
巡逻... 0 危险系数0
巡逻... 1 危险系数1
巡逻... 2 危险系数2
巡逻... 3 危险系数3
巡逻... 4 危险系数4
巡逻... 5 危险系数5
巡逻... 6 危险系数6
巡逻... 7 危险系数7
巡逻... 8 危险系数8
巡逻... 9 危险系数9
退出 巡逻
转化中 ...
进入 空闲
空闲... 0
空闲... 1
空闲... 2
空闲... 3
空闲... 4
空闲... 5
空闲... 6
空闲... 7
空闲... 8
空闲... 9
退出 空闲
转化中 ...
进入 巡逻
巡逻... 0 危险系数10
巡逻... 1 危险系数11
巡逻... 2 危险系数12
巡逻... 3 危险系数13
巡逻... 4 危险系数14
巡逻... 5 危险系数15
巡逻... 6 危险系数16
巡逻... 7 危险系数17
巡逻... 8 危险系数18
巡逻... 9 危险系数19
退出 巡逻
转化中 ...
进入 空闲
空闲... 0
空闲... 1
空闲... 2
空闲... 3
空闲... 4
空闲... 5
空闲... 6
空闲... 7
空闲... 8
空闲... 9
退出 空闲
转化中 ...
进入 巡逻
巡逻... 0 危险系数20
退出 巡逻
进入 逃跑
逃跑... 21
逃跑... 20
逃跑... 19
逃跑... 18
逃跑... 17
逃跑... 16
逃跑... 15
逃跑... 14
逃跑... 13
逃跑... 12
逃跑... 11
逃跑... 10
逃跑... 9
逃跑... 8
逃跑... 7
逃跑... 6
逃跑... 5
逃跑... 4
逃跑... 3
逃跑... 2
逃跑... 1
退出 逃跑
转化中 ...
进入 空闲
空闲... 0
空闲... 1
空闲... 2
空闲... 3
空闲... 4
空闲... 5
空闲... 6
空闲... 7
空闲... 8
空闲... 9
退出 空闲
转化中 ...
进入 巡逻
巡逻... 0 危险系数0
巡逻... 1 危险系数1
巡逻... 2 危险系数2
巡逻... 3 危险系数3
巡逻... 4 危险系数4
巡逻... 5 危险系数5
巡逻... 6 危险系数6
巡逻... 7 危险系数7
巡逻... 8 危险系数8
巡逻... 9 危险系数9
退出 巡逻
转化中 ...
进入 空闲
空闲... 0
空闲... 1
空闲... 2
空闲... 3
空闲... 4
空闲... 5
空闲... 6
空闲... 7
空闲... 8
空闲... 9
退出 空闲
转化中 ...
进入 巡逻
巡逻... 0 危险系数10
巡逻... 1 危险系数11
巡逻... 2 危险系数12
巡逻... 3 危险系数13
巡逻... 4 危险系数14
巡逻... 5 危险系数15
巡逻... 6 危险系数16
巡逻... 7 危险系数17
巡逻... 8 危险系数18
巡逻... 9 危险系数19
退出 巡逻
转化中 ...
进入 空闲
空闲... 0
空闲... 1
空闲... 2
空闲... 3
空闲... 4
空闲... 5
空闲... 6
空闲... 7
空闲... 8
空闲... 9
退出 空闲
转化中 ...
进入 巡逻
巡逻... 0 危险系数20
退出 巡逻
进入 逃跑
逃跑... 21
逃跑... 20
逃跑... 19
逃跑... 18
逃跑... 17
逃跑... 16
逃跑... 15
逃跑... 14
逃跑... 13
逃跑... 12
逃跑... 11
逃跑... 10
逃跑... 9
逃跑... 8
逃跑... 7
逃跑... 6
逃跑... 5
逃跑... 4
逃跑... 3
逃跑... 2
逃跑... 1
退出 逃跑
转化中 ...
进入 空闲
空闲... 0
空闲... 1
空闲... 2
空闲... 3
空闲... 4
空闲... 5
空闲... 6
空闲... 7
空闲... 8
空闲... 9
退出 空闲
转化中 ...
进入 巡逻
巡逻... 0 危险系数0
巡逻... 1 危险系数1
巡逻... 2 危险系数2
巡逻... 3 危险系数3
巡逻... 4 危险系数4
巡逻... 5 危险系数5
巡逻... 6 危险系数6
巡逻... 7 危险系数7
巡逻... 8 危险系数8
巡逻... 9 危险系数9
退出 巡逻
转化中 ...
进入 空闲
空闲... 0
空闲... 1
```