using System;
using System.Collections.Generic;
using System.Linq;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace NFlex.Test
{
    public class SpeechTest
    {
        [Fact]
        public void Test()
        {
            SpeechSynthesizer speech = new SpeechSynthesizer();
            speech.Rate = -2;
            speech.Volume = 100;
            speech.Speak("它具有以下特点。第一、具有良好的音质。它不是机械的模拟的电子声音，语音库中所有声音都是真人发音，语音清晰、悦耳。第二、它具有卓越的多音字识别系统，它通过词库来辨识多音字，并且对‘不’和‘一’这样的随位置不同而声调不同的多音字作了一定的处理。第三、它具有同 音乐的完美结合，它能在朗读文章的同时播放你所喜欢的音乐、歌曲。使用它后，两者在软件上不会互相冲突，声音绝对不会‘此起彼伏’，相互作用。第四、它对于朗读的各项参数具有开放性。用户可以调节所有有关朗读的参数，包括朗读的音量、频率、每个字的读音、每个读音的长度、以及多音字词组控制等等，如果可能的话用户还可以添加自己的语音库。第五、它支持多种文件格式，其中文本格式包括(TXT, GB, RTF, HTM, WPS) ，它还支持几种通用的音乐格式(MP3,WAV,MID,CDA)，此外它还支持BIG5码文件。第六、Read2U支持脚本功能。它能自动朗读一篇或多篇文章，能在背景中加入音乐伴奏并能实现定时提醒，自动关机等外部命令。");


            speech.Dispose();
        }
    }
}
