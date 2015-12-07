
'ИПОЛЬЗОВАНА БИБЛИОТЕКА Open Hardware Monitot
'http://openhardwaremonitor.org/downloads/
Imports OpenHardwareMonitor.Hardware

Module OhmTempExample
    Dim ohm As New Computer

    'ТЕМПЕРАТУРА ОТОБРАЖАЕТСЯ ТОЛЬКО ПРИ ЗАПУСКЕ ОТ АДМИНИСТРАТОРА!!!

    Sub OhmInit()
        'выбор нужных модулей
        ohm.CPUEnabled = True
        ohm.MainboardEnabled = False
        ohm.HDDEnabled = False
        ohm.GPUEnabled = False
        ohm.RAMEnabled = False
        'запуск
        ohm.Open()
    End Sub

    Sub FindOhmTemperature()
        'список модулей
        For Each hardware In ohm.Hardware
            'находим модуль процессора
            If hardware.HardwareType = HardwareType.CPU Then
                Console.WriteLine("CPU: " + hardware.Identifier.ToString + hardware.Name)
                'список сенсоров процессора
                For Each sensor In hardware.Sensors
                    'находим сенсоры температуры
                    If sensor.SensorType = SensorType.Temperature Then
                        Console.WriteLine("-- temperature: " + sensor.Name + ", " + sensor.Value.ToString)
                        'находим сенсор общей температуры
                        If sensor.Name.ToLower.Contains("package") Then
                            Console.WriteLine("Temperature Found: " + sensor.Name + ", " + sensor.Value.ToString)
                        End If
                    End If
                Next
            End If
        Next
    End Sub

    Sub SaveOhmReport()
        'получить готовый отчет в текстовом виде
        Dim textReport = ohm.GetReport
        IO.File.WriteAllText("report.txt", textReport)
    End Sub

    Sub Main()
        OhmInit()
        SaveOhmReport()
        FindOhmTemperature()
        Console.ReadKey()
    End Sub

End Module
