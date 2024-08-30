using RentCarMS__BlazorUI.Data.Models;
using System.Runtime.InteropServices;
using System.Net.Http.Headers;
namespace RentCarMS__BlazorUI.Services.Members
{
    public class MemberService : IMemberService
    {
        private readonly HttpClient httpClient;

        public MemberService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task CreateAsynce(Member mem)
        {
            var content = new MultipartFormDataContent();
            if (mem.ImageFile != null)
            {
                
                var fileContent = new StreamContent(mem.ImageFile.OpenReadStream());
                fileContent.Headers.ContentType = new MediaTypeHeaderValue(mem.ImageFile.ContentType);
                content.Add(fileContent, "ImageFile", mem.ImageFile.Name);
               
            }

            content.Add(new StringContent(mem.FastName ?? ""), "FastName");
            content.Add(new StringContent(mem.LastName ?? ""), "LastName");
            content.Add(new StringContent(mem.DOB.HasValue ? mem.DOB.Value.ToString("o") : ""), "DOB");
            content.Add(new StringContent(mem.Email ?? ""), "Email");
            content.Add(new StringContent(mem.Phone ?? ""), "Phone");
            content.Add(new StringContent(mem.JoinDate.HasValue ? mem.JoinDate.Value.ToString("o") : ""), "JoinDate");

            var response = await httpClient.PostAsync("api/Member", content);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Failed to create member");
            }
        }

        //public async Task CreateAsynce(Member mem)
        //{
        //    await httpClient.PostAsJsonAsync("api/Member", mem);
        //}

        public async Task DeleteAsynce(int id)
        {
            await httpClient.DeleteAsync($"api/Member/{id}");
        }

        public async Task<IEnumerable<Member>> GetAllAsynce()
        {
          return  await httpClient.GetFromJsonAsync<Member[]>("api/Member");
        }

        public async Task<Member> GetByIdAsynce(int id)
        {
            return await httpClient.GetFromJsonAsync<Member>($"api/Member/{id}");
        }

        public async Task UpdateAsync(Member memberDto)
        {
            using var content = new MultipartFormDataContent();

            if (memberDto.ImageFile != null)
            {
                var fileContent = new StreamContent(memberDto.ImageFile.OpenReadStream());
                fileContent.Headers.ContentType = new MediaTypeHeaderValue(memberDto.ImageFile.ContentType);
                content.Add(fileContent, "ImageFile", memberDto.ImageFile.Name);
            }

            content.Add(new StringContent(memberDto.FastName ?? string.Empty), "FastName");
            content.Add(new StringContent(memberDto.LastName ?? string.Empty), "LastName");
            content.Add(new StringContent(memberDto.DOB.HasValue ? memberDto.DOB.Value.ToString("o") : string.Empty), "DOB");
            content.Add(new StringContent(memberDto.Email ?? string.Empty), "Email");
            content.Add(new StringContent(memberDto.Phone ?? string.Empty), "Phone");
            content.Add(new StringContent(memberDto.ImagePath ?? string.Empty), "ImagePath");
            content.Add(new StringContent(memberDto.JoinDate.HasValue ? memberDto.JoinDate.Value.ToString("o") : string.Empty), "JoinDate");
            content.Add(new StringContent(memberDto.MemberID.ToString()), "MemberID");

            var response = await httpClient.PutAsync($"api/Member/{memberDto.MemberID}", content);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to update member. Status code: {response.StatusCode}");
            }
        }

    }


}
