import { Component } from '@angular/core';

class Artist {
  id: number;
  name: string;
}

@Component({
    moduleId: module.id,
    selector: 'artist-list',
    templateUrl: './artist-list.component.html'
})
export class ArtistListComponent {
  artists: Artist[] = [
    {
      id: 1,
      name: 'Rush'
    },
    {
      id: 2,
      name: 'Metallica'
    },
    {
      id: 3,
      name: 'Van Halen'
    },
    {
      id: 4,
      name: 'Megadeth'
    },
    {
      id: 5,
      name: 'Dream Theater'
    },
    {
      id: 6,
      name: 'Joe B'
    }

  ];
  constructor() {
  }
}
