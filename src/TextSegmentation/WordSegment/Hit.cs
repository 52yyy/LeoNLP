using System;

namespace WordSegment
{
    public class Hit
    {
        //Hit不匹配
        private const int UNMATCH = 0x00000000;
        //Hit完全匹配
        private const int MATCH = 0x00000001;
        //Hit前缀匹配
        private const int PREFIX = 0x00000010;


        /// <summary>
        /// 该HIT当前状态，默认未匹配
        /// </summary>
        private int _hitState = UNMATCH;

        /// <summary>
        /// 记录词典匹配过程中，当前匹配到的词典分支节点
        /// </summary>
        private DictSegment _matchedDictSegment;
        //词的起始位置
        private int _begin;
        //词的结束位置
        private int _end;

        public Boolean IsMatch()
        {
            return (_hitState & MATCH) > 0;
        }

        public void SetMatch()
        {
            _hitState = _hitState | MATCH;
        }

        public bool IsPrefix()
        {
            return (_hitState & PREFIX) > 0;
        }

        public void SetPrefix()
        {
            _hitState = _hitState | PREFIX;
        }

        public bool IsUnmatch()
        {
            return _hitState == UNMATCH;
        }

        public void SetUnmatch()
        {
            _hitState = UNMATCH;
        }

        public DictSegment GetMatchedDictSegment()
        {
            return _matchedDictSegment;
        }

        public void SetMatchedDictSegment(DictSegment matchedDictSegment)
        {
            _matchedDictSegment = matchedDictSegment;
        }

        public int GetBegin()
        {
            return _begin;
        }

        public void SetBegin(int begin)
        {
            _begin = begin;
        }

        public int GetEnd()
        {
            return _end;
        }

        public void SetEnd(int end)
        {
            _end = end;
        }
    }
}
