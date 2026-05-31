import { Component, OnInit, signal, computed, inject } from '@angular/core';
import { CatService } from './services/cat.service';
import { Breed } from './models/breed.model';
import { CatImage } from './models/cat-image.model';

@Component({
  selector: 'app-root',
  imports: [],
  templateUrl: './app.html',
  styleUrl: './app.scss'
})
export class App implements OnInit {
  private catService = inject(CatService);

  breeds = signal<Breed[]>([]);
  images = signal<CatImage[]>([]);
  selectedBreedId = signal<string>('');
  limit = signal<number>(10);
  loading = signal<boolean>(false);
  error = signal<string | null>(null);
  limitError = signal<string | null>(null);
  searched = signal<boolean>(false);
  selectedBreed = computed(() => this.breeds().find(b => b.id === this.selectedBreedId()) ?? null);

  ngOnInit() {
    this.catService.getBreeds().subscribe({
      next: (data) => this.breeds.set(data),
      error: () => this.error.set('No se pudieron cargar las razas.')
    });
  }

  onBreedChange(event: Event) {
    this.selectedBreedId.set((event.target as HTMLSelectElement).value);
    this.images.set([]);
    this.error.set(null);
    this.searched.set(false);
  }

  onLimitChange(event: Event) {
    const value = parseInt((event.target as HTMLInputElement).value, 10);
    if (isNaN(value) || value < 1) {
      this.limitError.set('El campo no puede estar vacío ni ser menor a 1.');
    } else {
      this.limitError.set(null);
      this.limit.set(value);
    }
  }

  search() {
    if (!this.selectedBreedId()) return;
    this.loading.set(true);
    this.error.set(null);
    this.images.set([]);
    this.searched.set(true);

    this.catService.getImages(this.selectedBreedId(), this.limit()).subscribe({
      next: (data) => {
        this.images.set(data);
        this.loading.set(false);
      },
      error: () => {
        this.error.set('No se pudieron cargar las imágenes.');
        this.loading.set(false);
      }
    });
  }
}
