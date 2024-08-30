using Microsoft.AspNetCore.Hosting;

using Microsoft.Extensions.Hosting;
using RentCarMS_CleanArchitecture.Application.DTO_s;
using RentCarMS_CleanArchitecture.Domain.Members;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCarMS_CleanArchitecture.Application.Members.Service
{
    public class MemberService
    {
        private readonly IMemberRepository _repository;
        private readonly IWebHostEnvironment _hostEnvironment;
        public MemberService(IMemberRepository repository, IWebHostEnvironment hostEnvironment)
        {
            _repository = repository;
            _hostEnvironment = hostEnvironment;

        }
        public async Task<IEnumerable<MemberDTO>> GetAllMember()
        {
            var members = await _repository.GetAllAsync();
            return members.Select(m => new MemberDTO
            {
                MemberID = m.MemberID,
                FastName = m.FastName,
                LastName = m.LastName,
                DOB = m.DOB,
                Email = m.Email,
                Phone = m.Phone,
                ImagePath = m.ImagePath,
                JoinDate = m.JoinDate
            }).ToList();
        }

        public async Task<MemberDTO> GetMemberById(int id)
        {
            var members = await _repository.GetByIdAsync(id);
            return new MemberDTO
            {
                MemberID = members.MemberID,
                FastName = members.FastName,
                LastName = members.LastName,
                DOB = members.DOB,
                Email = members.Email,
                Phone = members.Phone,
                ImagePath = members.ImagePath,
                JoinDate = members.JoinDate

            };
        }

        public async Task<MemberDTO> CreateMemberAsync(MemberDTO memberDto)
        {
            if (memberDto.ImageFile != null && memberDto.ImageFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath ?? string.Empty, "uploads");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(memberDto.ImageFile.FileName);
                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await memberDto.ImageFile.CopyToAsync(fileStream);
                }
                var baseUrl = "https://localhost:7259";
                memberDto.ImagePath = baseUrl +"/"+ uploadsFolder +"/"+ fileName;

                //memberDto.ImagePath = $"{baseUrl}/uploads/{fileName}";
                //memberDto.ImagePath = fileName;
            }
            var member = new Member
            {
                FastName = memberDto.FastName,
                LastName = memberDto.LastName,
                DOB = memberDto.DOB,
                Email = memberDto.Email,
                Phone = memberDto.Phone,
                ImagePath = memberDto.ImagePath,
                JoinDate = memberDto.JoinDate
            };
             
            var createmember = await _repository.CreateAsync(member);
            memberDto.MemberID = createmember.MemberID;
            return memberDto;

        }

        public async Task<MemberDTO> UpdateMemberAsync(MemberDTO memberDto)
        {
            var existingEntity = await _repository.GetByIdAsync(memberDto.MemberID);
            //var existingEntity = await GetMemberById(member.MemberID);
            if (existingEntity == null)
            {
                return null;
            }
            existingEntity.FastName = memberDto.FastName;
            existingEntity.LastName = memberDto.LastName;
            existingEntity.DOB = memberDto.DOB;
            existingEntity.Email = memberDto.Email;
            existingEntity.Phone = memberDto.Phone;
            existingEntity.JoinDate = memberDto.JoinDate;

            if (memberDto.ImageFile != null && memberDto.ImageFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath ?? string.Empty, "uploads");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                // Remove the previous image
                if (!string.IsNullOrEmpty(existingEntity.ImagePath))
                {
                    var previousImagePath = Path.Combine(uploadsFolder, existingEntity.ImagePath);
                    if (System.IO.File.Exists(previousImagePath))
                    {
                        System.IO.File.Delete(previousImagePath);
                    }
                }

                // Save the new image
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(memberDto.ImageFile.FileName);
                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await memberDto.ImageFile.CopyToAsync(fileStream);
                }
                var baseUrl = "https://localhost:7259";
                existingEntity.ImagePath = baseUrl + "/" + uploadsFolder + "/" + fileName;
               // existingEntity.ImagePath = fileName;
            }
            await _repository.UpdateAsync(existingEntity);
            memberDto.ImagePath = existingEntity.ImagePath;
            return memberDto;
           
        }

        public async Task<bool> DeleteMemberAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}
