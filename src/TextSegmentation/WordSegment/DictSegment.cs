using System;
using System.Collections;
using System.Linq;

namespace WordSegment
{
    public class DictSegment:IComparable<DictSegment>
    {
        // 公用字典表，存储汉字
        private static readonly Hashtable CharMap = new Hashtable();
        // 数组大小上限
        private const int ArrayLengthLimit = 3;
        // Map存储结构
        private Hashtable _childrenMap;
        // 数组方式存储结构
        private DictSegment[] _childrenArray;
        // 当前节点上存储的字符
        private readonly char _nodeChar;
        // 当前节点存储的Segment数目
        // storeSize <=ARRAY_LENGTH_LIMIT ，使用数组存储， storeSize >ARRAY_LENGTH_LIMIT
        // ,则使用Map存储
        private int _storeSize = 0;
        // 当前DictSegment状态 ,默认 0 , 1表示从根节点到当前节点的路径表示一个词
        private int _nodeState = 0;


        public DictSegment(char nodeChar)
        {
            _nodeChar = nodeChar;
        }

        public char GetNodeChar()
        {
            return _nodeChar;
        }
        /// <summary>
        /// 判断是否有下一节点
        /// </summary>
        /// <returns></returns>
        bool HasNextNode()
        {
            return _storeSize > 0;
        }
        /// <summary>
        /// 匹配词段
        /// </summary>
        /// <param name="charArray"></param>
        /// <returns></returns>
        public Hit Match(char[] charArray)
        {
            return Match(charArray, 0, charArray.Length, null);
        }
        /// <summary>
        /// 匹配词段
        /// </summary>
        /// <param name="charArray"></param>
        /// <param name="begin"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public Hit Match(char[] charArray, int begin, int length)
        {
            return Match(charArray, begin, length, null);
        }
        /// <summary>
        /// 匹配词段
        /// </summary>
        /// <param name="charArray"></param>
        /// <param name="begin"></param>
        /// <param name="length"></param>
        /// <param name="searchHit"></param>
        /// <returns></returns>
        public Hit Match(char[] charArray, int begin, int length, Hit searchHit)
        {
            if (searchHit == null)
            {
                // 如果hit为空，新建
                searchHit = new Hit();
                // 设置hit的其实文本位置
                searchHit.SetBegin(begin);
            }
            else
            {
                // 否则要将HIT状态重置
                searchHit.SetUnmatch();
            }
            // 设置hit的当前处理位置
            searchHit.SetEnd(begin);

            char keyChar = charArray[begin];
            DictSegment ds = null;

            // 引用实例变量为本地变量，避免查询时遇到更新的同步问题
            DictSegment[] segmentArray = _childrenArray;
            Hashtable segmentMap = _childrenMap;

            // STEP1 在节点中查找keyChar对应的DictSegment
            if (segmentArray != null)
            {
                // 在数组中查找
                var keySegment = new DictSegment(keyChar);
                int position = BinarySearch(segmentArray, 0, _storeSize, keySegment);
                if (position >= 0)
                {
                    ds = segmentArray[position];
                }

            }
            else if (segmentMap != null)
            {
                // 在map中查找
                ds = (DictSegment)segmentMap[keyChar];
            }

            // STEP2 找到DictSegment，判断词的匹配状态，是否继续递归，还是返回结果
            if (ds != null)
            {
                if (length > 1)
                {
                    // 词未匹配完，继续往下搜索
                    return ds.Match(charArray, begin + 1, length - 1, searchHit);
                }
                if (length == 1)
                {

                    // 搜索最后一个char
                    if (ds._nodeState == 1)
                    {
                        // 添加HIT状态为完全匹配
                        searchHit.SetMatch();
                    }
                    if (ds.HasNextNode())
                    {
                        // 添加HIT状态为前缀匹配
                        searchHit.SetPrefix();
                        // 记录当前位置的DictSegment
                        searchHit.SetMatchedDictSegment(ds);
                    }
                    return searchHit;
                }

            }
            // STEP3 没有找到DictSegment， 将HIT设置为不匹配
            return searchHit;
        }

        /// <summary>
        /// 加载填充词典片段
        /// </summary>
        /// <param name="charArray"></param>
        public void FillSegment(char[] charArray)
        {
            FillSegment(charArray, 0, charArray.Length, 1);
        }

        /// <summary>
        /// 屏蔽词典中的一个词
        /// </summary>
        /// <param name="charArray"></param>
        void DisableSegment(char[] charArray)
        {
            FillSegment(charArray, 0, charArray.Length, 0);
        }
        /// <summary>
        /// 填充词典片段
        /// </summary>
        /// <param name="charArray"></param>
        /// <param name="begin"></param>
        /// <param name="length"></param>
        /// <param name="enabled"></param>
        public void FillSegment(Char[] charArray, int begin, int length, int enabled)
        {
            char beginChar = charArray[begin];

            char keyChar = beginChar;
            if (!CharMap.ContainsKey(keyChar))
            {
                CharMap.Add(beginChar, beginChar);
                keyChar = beginChar;
            }

            // 搜索当前节点的存储，查询对应keyChar的keyChar，如果没有则创建
            DictSegment ds = LookforSegment(keyChar, enabled);

            if (ds != null)
            {
                // 处理keyChar对应的segment
                if (length > 1)
                {
                    // 词元还没有完全加入词典树
                    ds.FillSegment(charArray, begin + 1, length - 1, enabled);
                }
                else if (length == 1)
                {
                    // 已经是词元的最后一个char,设置当前节点状态为enabled，
                    // enabled=1表明一个完整的词，enabled=0表示从词典中屏蔽当前词
                    ds._nodeState = enabled;
                }
            }
        }

        /// <summary>
        /// 查找本节点下对应的keyChar的segment
        /// </summary>
        /// <param name="keyChar"></param>
        /// <param name="create"></param>
        /// <returns></returns>
        private DictSegment LookforSegment(Char keyChar, int create)
        {
            DictSegment ds = null;
            if (_storeSize <= ArrayLengthLimit)
            {
                // 获取数组容器，如果数组未创建则创建数组
                DictSegment[] segmentArray = GetChildrenArray();
                // 搜寻数组
                var keySegment = new DictSegment(keyChar);
                int position = binarySearch(segmentArray, 0, _storeSize, keySegment);
                //int position = BinarySearch(segmentArray, keySegment, 0, this.storeSize);
                if (position >= 0)
                {
                    ds = segmentArray[position];
                }

                // 遍历数组后没有找到对应的segment
                if (ds == null && create == 1)
                {
                    ds = keySegment;
                    if (_storeSize < ArrayLengthLimit)
                    {
                        // 数组容量未满，使用数组存储
                        segmentArray[_storeSize] = ds;
                        // segment数目+1
                        _storeSize++;
                        segmentArray.OrderBy(p => p._nodeChar);
                        //Arrays.sort(segmentArray, 0, this.storeSize);

                    }
                    else
                    {
                        // 数组容量已满，切换Map存储
                        // 获取Map容器，如果Map未创建,则创建Map
                        Hashtable segmentMap = GetChildrenMap();
                        // 将数组中的segment迁移到Map中
                        Migrate(segmentArray, segmentMap);
                        // 存储新的segment
                        segmentMap.Add(keyChar, ds);
                        // segment数目+1 ， 必须在释放数组前执行storeSize++ ， 确保极端情况下，不会取到空的数组
                        _storeSize++;
                        // 释放当前的数组引用
                        _childrenArray = null;
                    }
                }
            }
            else
            {
                // 获取Map容器，如果Map未创建,则创建Map
                Hashtable segmentMap = GetChildrenMap();
                // 搜索Map
                ds = (DictSegment)segmentMap[keyChar];
                if (ds == null && create == 1)
                {
                    // 构造新的segment
                    ds = new DictSegment(keyChar);
                    segmentMap.Add(keyChar, ds);
                    // 当前节点存储segment数目+1
                    _storeSize++;
                }
            }

            return ds;
        }

        /// <summary>
        /// 获取数组容器 线程同步方法
        /// </summary>
        /// <returns></returns>
        private DictSegment[] GetChildrenArray()
        {
            if (_childrenArray == null)
            {
                if (_childrenArray == null)
                {
                    _childrenArray = new DictSegment[ArrayLengthLimit];
                }

            }
            return _childrenArray;
        }
        /// <summary>
        /// 获取Map容器
        /// </summary>
        /// <returns></returns>
        private Hashtable GetChildrenMap()
        {
            if (_childrenMap == null)
            {
                _childrenMap = new Hashtable();
            }
            return _childrenMap;
        }
        /// <summary>
        /// 将数组中的Segment迁移到Map中
        /// </summary>
        /// <param name="segmentArray"></param>
        /// <param name="segmentMap"></param>
        private void Migrate(DictSegment[] segmentArray, Hashtable segmentMap)
        {
            for (int i = 0; i < segmentArray.Length; i++)
            {
                if (!segmentArray[i].Equals(null))
                {
                    segmentMap.Add(segmentArray[i]._nodeChar, segmentArray[i]);
                }
            }
        }

        /// <summary>
        /// 实现IComparable接口
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public int CompareTo(DictSegment o)
        {
            // 对当前节点存储的char进行比较
            return _nodeChar.CompareTo(o._nodeChar);
        }
        /// <summary>
        /// 二分查找
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="low"></param>
        /// <param name="high"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        private static int BinarySearch(DictSegment[] arr, int low, int high, DictSegment key)
        {
            int mid = (low + high) / 2;
            if (low >= high)
            {
                return -1;
            }
            if (arr[mid]._nodeChar == key._nodeChar)
            {
                return mid;
            }
            if (arr[mid]._nodeChar > key._nodeChar)
            {
                return BinarySearch(arr, low, mid - 1, key);
            }
            return BinarySearch(arr, mid + 1, high, key);

        }
        /// <summary>
        /// 二分查找
        /// </summary>
        /// <param name="a"></param>
        /// <param name="key"></param>
        /// <param name="low"></param>
        /// <param name="high"></param>
        /// <returns></returns>
        public static int BinarySearch(DictSegment[] a, DictSegment key, int low, int high)
        {
            while (low <= high)
            {
                int mid = (low + high) >> 1;

                DictSegment midVal = a[mid];
                if (midVal == null)
                {
                    return -1;
                }
                int cmp = midVal.CompareTo(key);

                if (cmp < 0)
                {
                    low = mid + 1;
                }
                else if (cmp > 0)
                {
                    high = mid - 1;
                }
                else
                {
                    return mid; // key found
                }
            }
            return -(low + 1);  // key not found.
        }
        /// <summary>
        /// RangeCheck
        /// </summary>
        /// <param name="paramInt1"></param>
        /// <param name="paramInt2"></param>
        /// <param name="paramInt3"></param>
        private static void RangeCheck(int paramInt1, int paramInt2, int paramInt3)
        {
            if (paramInt2 > paramInt3)
            {
                throw new ArgumentException("fromIndex(" + paramInt2 + ") > toIndex(" + paramInt3 + ")");
            }
            if (paramInt2 < 0)
            {
                throw new IndexOutOfRangeException(paramInt2.ToString());
            }
            if (paramInt3 > paramInt1)
            {
                throw new IndexOutOfRangeException(paramInt3.ToString());
            }
        }

        public static int binarySearch(DictSegment[] paramArrayOfObject, int paramInt1, int paramInt2, DictSegment paramObject)
        {
            RangeCheck(paramArrayOfObject.Length, paramInt1, paramInt2);
            return binarySearch0(paramArrayOfObject, paramInt1, paramInt2, paramObject);
        }

        private static int binarySearch0(DictSegment[] paramArrayOfObject, int paramInt1, int paramInt2, DictSegment paramObject)
        {
            int i = paramInt1;
            int j = paramInt2 - 1;
            while (i <= j)
            {
                int k = i + j >> 1;
                DictSegment localComparable = paramArrayOfObject[k];
                int m = localComparable.CompareTo(paramObject);
                if (m < 0)
                {
                    i = k + 1;
                }
                else if (m > 0)
                {
                    j = k - 1;
                }
                else
                {
                    return k;
                }
            }
            return -(i + 1);
        }

    }
}
