import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AltaCobranzaComponent } from './alta-cobranza.component';

describe('AltaCobranzaComponent', () => {
  let component: AltaCobranzaComponent;
  let fixture: ComponentFixture<AltaCobranzaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AltaCobranzaComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AltaCobranzaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
