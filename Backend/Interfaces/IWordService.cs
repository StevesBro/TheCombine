﻿using System.Collections.Generic;
using System.Threading.Tasks;
using BackendFramework.Models;

namespace BackendFramework.Interfaces
{
    public interface IWordService
    {
        Task<bool> Update(string projectId, string wordId, Word word);
        Task<bool> Delete(string projectId, string wordId);
        Task<List<Word>> Merge(string projectId, MergeWords mergeWords);
        Task<bool> WordIsUnique(Word word);
        string GetAudioFilePath(string projectId, string wordId, string fileName);
    }
}
