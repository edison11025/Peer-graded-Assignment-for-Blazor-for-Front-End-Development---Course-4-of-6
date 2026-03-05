using System.Collections.Generic;
using System.Linq;

namespace EventEaseApp2.Services
{
    public class EventService
    {
        private readonly Dictionary<int, HashSet<string>> _attendance = new();
        private readonly Dictionary<int, List<Comment>> _comments = new();
        private readonly object _lock = new();

        public void RegisterAttendance(int eventId, string userName)
        {
            if (string.IsNullOrWhiteSpace(userName)) return;
            lock (_lock)
            {
                if (!_attendance.ContainsKey(eventId))
                    _attendance[eventId] = new HashSet<string>();
                _attendance[eventId].Add(userName);
            }
        }

        public IReadOnlyList<string> GetAttendance(int eventId)
        {
            lock (_lock)
            {
                if (_attendance.TryGetValue(eventId, out var set))
                    return set.ToList();
            }
            return new List<string>();
        }

        public void AddComment(int eventId, string comment, string email)
        {
            if (string.IsNullOrWhiteSpace(comment) || string.IsNullOrWhiteSpace(email)) return;
            lock (_lock)
            {
                if (!_comments.ContainsKey(eventId))
                    _comments[eventId] = new List<Comment>();
                _comments[eventId].Add(new Comment { Text = comment, Email = email });
            }
        }

        public IReadOnlyList<Comment> GetComments(int eventId)
        {
            lock (_lock)
            {
                if (_comments.TryGetValue(eventId, out var list))
                    return list.ToList();
            }
            return new List<Comment>();
        }
    }

    public class Comment
    {
        public string Text { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}