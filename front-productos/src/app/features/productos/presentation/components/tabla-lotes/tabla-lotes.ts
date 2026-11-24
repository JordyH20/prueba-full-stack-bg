import { Component, input } from '@angular/core';
import { DecimalPipe, DatePipe } from '@angular/common';
import { Lote } from '../../../domain/models/lote';

@Component({
  selector: 'app-tabla-lotes',
  imports: [DecimalPipe, DatePipe],
  templateUrl: './tabla-lotes.html',
  styleUrl: './tabla-lotes.css'
})
export class TablaLotesComponent {
  readonly lotes = input.required<Lote[]>();
}
