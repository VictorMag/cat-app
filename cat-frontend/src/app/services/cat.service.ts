import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Breed } from '../models/breed.model';
import { CatImage } from '../models/cat-image.model';

const API_BASE = 'http://localhost:5223/api/cats';

@Injectable({ providedIn: 'root' })
export class CatService {
  private http = inject(HttpClient);

  getBreeds(): Observable<Breed[]> {
    return this.http.get<Breed[]>(`${API_BASE}/breeds`);
  }

  getImages(breedId: string, limit: number): Observable<CatImage[]> {
    return this.http.get<CatImage[]>(`${API_BASE}/images?breedId=${breedId}&limit=${limit}`);
  }
}
