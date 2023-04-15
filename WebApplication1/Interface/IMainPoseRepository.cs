﻿using WebApplication1.Models;

namespace WebApplication1.Interface
{
    public interface IMainPoseRepository
    {
        Task<IEnumerable<MainPose>> GetAll();
        Task<IEnumerable<Comment>> GetCommentsByMainPoseId(int mainPoseId);
        Task<MainPose> GetByIdAsync(int id);
        Task AddComment(Comment comment);
        Task AddPose(MainPose pose);
        bool Add(MainPose mainPose);
        bool Delete(MainPose mainPose);
        bool Update(MainPose mainPose);
        bool Save();
    }
}