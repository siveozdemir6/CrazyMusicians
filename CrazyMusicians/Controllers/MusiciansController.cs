using CrazyMusicians.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;




namespace CrazyMusicians.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MusiciansController : ControllerBase
    {
        public static List<CrazyMusician> musicians = new List<CrazyMusician>

        {
            new CrazyMusician { Id = 1, Name = "Ahmet Çalgı", Profession = "Ünlü Çalgı Çalar", FunFact = "Her zaman yanlış nota çalar, ama çok eğlenceli" },
            new CrazyMusician { Id = 2, Name = "Zeynep Melodi", Profession = "Popüler Melodi Yazarı", FunFact = "Şarkıları yanlış anlaşılır ama çok popüler" },
            new CrazyMusician { Id = 3, Name = "Cemil Akor", Profession = "Çılgın Akorist", FunFact = "Akorları sık değiştirir, ama şaşırtıcı derecede yetenekli" },
            new CrazyMusician { Id = 4, Name = "Fatma Nota", Profession = "Sürpriz Nota Üreticisi", FunFact = "Nota üretirken sürekli sürprizler hazırlar" },
            new CrazyMusician { Id = 5, Name = "Hasan Ritim", Profession = "Ritim Canavarı", FunFact = "Her ritmi kendi tarzında yapar, hiç uymaz ama komiktir" },
            new CrazyMusician { Id = 6, Name = "Elif Armoni", Profession = "Armoni Ustası", FunFact = "Armonilerini bazen yanlış çalar, ama çok yaratıcıdır" },
            new CrazyMusician { Id = 7, Name = "Ali Perde", Profession = "Perde Uygulayıcı", FunFact = "Her perdeyi farklı şekilde çalar, her zaman sürprizlidir" },
            new CrazyMusician { Id = 8, Name = "Ayşe Rezonans", Profession = "Rezonans Uzmanı", FunFact = "Rezonans konusunda uzman, ama bazen çok gürültü çıkarır" },
            new CrazyMusician { Id = 9, Name = "Murat Ton", Profession = "Tonlama Meraklısı", FunFact = "Tonlamalarındaki farklılıklar bazen komik, ama oldukça ilginç" },
            new CrazyMusician { Id = 10, Name = "Selin Akor", Profession = "Akor Sihirbazı", FunFact = "Akorları değiştirdiğinde bazen sihirli bir hava yaratır" }
        };


        // GET: api/musicians
        [HttpGet]
        public ActionResult <IEnumerable<CrazyMusician>> Get()
        {
            return Ok (musicians);
        }

        // GET: api/musicians/5
        [HttpGet("{id}")]
        public ActionResult<CrazyMusician> GetById(int id)
        {
            var musician = musicians.FirstOrDefault(m => m.Id == id);
            if (musician == null)
            {
                return NotFound();
            }
            return Ok(musician);
        }

        // POST: api/musicians
        [HttpPost]
        public ActionResult<CrazyMusician> Post([FromBody] CrazyMusician newMusician)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            newMusician.Id = musicians.Max(m => m.Id) + 1;
            musicians.Add(newMusician);
            return CreatedAtAction(nameof(GetById), new { id = newMusician.Id }, newMusician);


        }

        // PUT: api/musicians/5
        [HttpPut("{id}")]
        public ActionResult<CrazyMusician> Put(int id, [FromBody] CrazyMusician updatedMusician)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var musician = musicians.FirstOrDefault(m => m.Id == id);
            if (musician == null)
            {
                return NotFound();
            }
            musician.Name = updatedMusician.Name;
            musician.Profession = updatedMusician.Profession;
            musician.FunFact = updatedMusician.FunFact;
            return Ok(musician);
        }

        //Patch: api/musicians/5
        [HttpPatch("{id}")]

        public ActionResult<CrazyMusician> Patch(int id, [FromBody] JsonPatchDocument<CrazyMusician> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }
            var musician = musicians.FirstOrDefault(m => m.Id == id);
            if (musician == null)
            {
                return NotFound();
            }
            patchDoc.ApplyTo(musician, ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(musician);
        }




        // DELETE: api/musicians/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var musician = musicians.FirstOrDefault(m => m.Id == id);
            if (musician == null)
            {
                return NotFound();
            }
            musicians.Remove(musician);
            return NoContent();
        }
        // GET: api/musicians/search?name=selin
        [HttpGet("search")]
        public ActionResult<IEnumerable<CrazyMusician>> Search([FromQuery] string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return BadRequest("You must provide a name to search.");
            }

            var result = musicians
                .Where(m => m.Name.Contains(name, StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (result.Count == 0)
                return NotFound("No musicians found with that name.");

            return Ok(result);
        }

    }
}
