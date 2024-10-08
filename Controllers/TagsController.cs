﻿namespace DekaNews.Controllers;

public class TagsController : Controller
{
    private readonly ApplicationDbContext _context;

    public TagsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Tags
    public async Task<IActionResult> Index()
    {
        return View(await _context.Tags.ToListAsync());
    }

    // GET: Tags/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id != null)
        {
            var tag = await _context.Tags
                .FirstOrDefaultAsync(m => m.Id == id);
            return tag == null ? NotFound() : View(tag);
        }

        return NotFound();
    }

    // GET: Tags/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Tags/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Descricao")] Tag tag)
    {
        if (ModelState.IsValid)
        {
            _context.Add(tag);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(tag);
    }

    // GET: Tags/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id != null)
        {
            var tag = await _context.Tags.FindAsync(id);
            return tag == null ? NotFound() : View(tag);
        }

        return NotFound();
    }

    // POST: Tags/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Descricao")] Tag tag)
    {
        if (id == tag.Id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tag);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TagExists(tag.Id))
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
            return View(tag);
        }

        return NotFound();
    }

    // GET: Tags/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id != null)
        {
            var tag = await _context.Tags
                .FirstOrDefaultAsync(m => m.Id == id);
            return tag == null ? NotFound() : View(tag);
        }

        return NotFound();
    }

    // POST: Tags/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var tag = await _context.Tags.FindAsync(id);
        if (tag != null)
            _context.Tags.Remove(tag);
        
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool TagExists(int id)
    {
        return _context.Tags.Any(e => e.Id == id);
    }
}

