namespace DekaNews.Controllers;

public class NoticiasController : Controller
{
    private readonly ApplicationDbContext _context;

    public NoticiasController(ApplicationDbContext context)
    {
        _context = context;
    }
    // GET: Noticias
    public async Task<IActionResult> Index()
    {
        var applicationDbContext = _context.Noticias
            .Include(c => c.Usuario)
            .Include(t => t.Tags);
        return View(await applicationDbContext.ToListAsync());
    }

    // GET: Noticias/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id != null)
        {
            var noticia = await GetNoticia(id);
            return noticia == null ? NotFound() : View(noticia);
        }
        return NotFound();
    }

    // GET: Noticias/Create
    [Authorize]
    public async Task<IActionResult> Create()
    {
        NoticiaViewModel viewModel = new() { SelectedTags = PopulateTags() };

        return View(viewModel);
    }

    // POST: Noticias/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize]
    public async Task<IActionResult> Create(IFormCollection form)
    {
        Noticia noticia = new()
        {
            Titulo = form["Titulo"]!,
            Texto = form["Texto"]!,
            UsuarioId = User.Claims.FirstOrDefault()!.Value
        };

        var tags = form["tagIds"].ToList();

        foreach (string tagId in tags)
        {
            var tag = _context.Tags.FirstOrDefault(f => f.Id.ToString() == tagId);
            noticia.Tags.Add(tag);
        }

        if (ModelState.IsValid)
        {
            _context.Add(noticia);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(noticia);
    }

    // GET: Noticias/Edit/5
    [Authorize]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id != null)
        {
            Noticia? noticia = await GetNoticia(id);
            if (noticia != null)
            {
                NoticiaViewModel viewModel = new()
                {
                    SelectedTags = PopulateTags(noticia.Tags),
                    Id = noticia.Id,
                    Texto = noticia.Texto,
                    Titulo = noticia.Titulo,
                    TagIds = noticia.Tags.Select(n => n.Id.ToString()).ToList()!
                };
                return View(viewModel);
            }
        }
        return NotFound();
    }

    // POST: Noticias/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Titulo,Texto")] Noticia noticia, NoticiaViewModel form)
    {
        if (id == noticia.Id)
        {
            var itemNoticia = await GetNoticia(id);
            var tags = GetTags();

            if (ModelState.IsValid)
            {
                try
                {
                    itemNoticia!.Titulo = noticia.Titulo;
                    itemNoticia.Texto = noticia.Texto;
                    UpdateNoticiaTags(form, itemNoticia, tags);

                    _context.Noticias.Update(itemNoticia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    if (!NoticiaExists(noticia.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(noticia);
        }
        return NotFound();
    }
    static void UpdateNoticiaTags(NoticiaViewModel form, Noticia itemNoticia, List<Tag> tags)
    {
        foreach (var item in tags)
        {
            itemNoticia.Tags.Remove(item);

            foreach (var tagId in form.TagIds)
            {
                if (tagId == item.Id.ToString())
                    itemNoticia.Tags.Add(item);
            }
        }
    }

    // GET: Noticias/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id != null)
        {
            var noticia = await _context.Noticias
                .FirstOrDefaultAsync(m => m.Id == id);
            return noticia == null ? NotFound() : View(noticia);
        }

        return NotFound();
    }

    // POST: Noticias/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    [Authorize]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var noticia = await GetNoticia(id);
        if (noticia != null)
        {
            _context.Noticias.Remove(noticia);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private List<SelectListItem> PopulateTags(List<Tag> tags = null)
    {
        try
        {


            List<SelectListItem> list = [.. _context.Tags.Select(p =>
            new SelectListItem
            {
                Text = p.Descricao,
                Value = p.Id.ToString(),
            })];
            if (tags != null)
            {
                foreach (var item in list)
                {
                    if (tags.Exists(f => f.Id.ToString() == item.Value))
                    {
                        item.Selected = true;
                    }
                }
            }
            return list;
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    private bool NoticiaExists(int id)
    {
        return _context.Noticias.Any(e => e.Id == id);
    }
    async Task<Noticia?> GetNoticia(int? id)
    {
        return await _context.Noticias
            .Include(c => c.Tags)
            .FirstOrDefaultAsync(f => f.Id
            .Equals(id));
    }
    List<Tag> GetTags()
    {
        return _context.Tags.Include(c => c.Noticias).ToList();
    }
}
