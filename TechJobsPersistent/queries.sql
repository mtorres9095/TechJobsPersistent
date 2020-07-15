--Part 1
-- Id integer;
-- Name string;
-- EmployerId integer;

--Part 2
-- SELECT * FROM techjobs.employers
-- WHERE Location = "St. Louis City";

--Part 3
-- SELECT jobs.Name, skills.Name, skills.Description
-- FROM jobskills
-- INNER JOIN skills ON jobskills.SkillId = skills.Id 
-- INNER JOIN jobs ON jobs.Id = jobskills.JobId
-- ORDER BY jobs.Name ASC;