using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

public class Main : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // 主线程用来保存工作结果的安全系统数据
        NativeArray<float> result = new NativeArray<float>(1, Allocator.TempJob);

        // 创建一个作业
        MyJob jobData = new MyJob();
        jobData.a = 10;//拷贝处理数据到作业中
        jobData.b = 10;//拷贝处理数据到作业中
        jobData.result = result;//把作业中的结果引用指向主线程中的结果数据

        // 调度本作业
        JobHandle handle = jobData.Schedule();

        // 等待作业完成
        handle.Complete();

        //把作业处理后的结果拷贝到主线程变量中
        float aPlusB = result[0];

        //释放分配的内存
        result.Dispose();
        Debug.Log(aPlusB);
    }

   
}
